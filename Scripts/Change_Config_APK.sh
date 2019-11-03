#!/bin/sh
apkPath=$1
envName=$2
keystorePath=$3
keyAlias=$4
keystorePass=$5
minSdkVersion=$6
playStoreRelease=${7:-false}

echo "Update - CAUSER"
echo "PWD: $PWD"
echo "apkPath: $apkPath"
echo "envName: $envName"

echo "Installing Apktool"
brew list apktool &>/dev/null || HOMEBREW_NO_AUTO_UPDATE=1 brew install apktool

echo "Installing XMLStarlet"
brew list xmlstarlet &>/dev/null || HOMEBREW_NO_AUTO_UPDATE=1 brew install xmlstarlet

cp $apkPath $PWD

envConfig="appsettings.$envName.json"
echo "envConfig: $envConfig"

zipPath="$apkPath.zip"

echo "Copy $apkPath to $zipPath"
cp $apkPath $zipPath

bakPath="$apkPath.bak"

echo "Rename $apkPath to $bakPath"
mv $apkPath $bakPath

echo "Decompiling $zipPath"
apktool d $zipPath -o "apk"

keepFile="$envConfig.keep"

cd apk

echo "Rename $envConfig to $keepFile"
mv "assets/$envConfig" "assets/$keepFile"

echo "Remove .json files"
rm assets/*.json

echo "Rename .keep to appsettings.json"
mv "assets/$keepFile" "assets/appsettings.json"

if [[ ${playStoreRelease} != 'true' ]] ; then
    echo "Rename app for environment"
    friendlyEnvironmentName=$(echo ${envName} | tr -s '[:space:]-[:space:]' '_' | tr '[:space:]' '_' | tr '[:upper:]' '[:lower:]' | sed -e 's/_*$//')

    oldPackageName=$(xml sel -t -v "/manifest/@package" AndroidManifest.xml)

    newPackageName="$(xml sel -t -v "/manifest/@package" AndroidManifest.xml).${friendlyEnvironmentName}"
    echo "    rename package to [${newPackageName}]"
    xml ed --inplace -u "/manifest/@package" -v "${newPackageName}" AndroidManifest.xml

    oldAuthorityName=$(xml sel -t -v "/manifest/application/provider[last()]/@android:authorities" AndroidManifest.xml)
    newAuthorityName=${oldAuthorityName//$oldPackageName/$newPackageName}
    echo "    rename last authority to [${newAuthorityName}]"
    xml ed --inplace -u "/manifest/application/provider[last()]/@android:authorities" -v "${newAuthorityName}" AndroidManifest.xml

    newLabel="$(xml sel -t -v "/resources/string[@name='app_name']" res/values/strings.xml) ${envName}"
    echo "    rename android:label to [${envName}]"
    xml ed --inplace -u "/resources/string[@name='app_name']" -v "${envName}" res/values/strings.xml  
fi

unalignedPath="$apkPath.unaligned"
unsignedPath="$apkPath.unsigned"

cd ..

echo "Repackage APK to $unsignedPath"
apktool b apk -o $unsignedPath

echo "Sign APK"
jarsigner -keystore $keystorePath -storepass $keystorePass -keypass $keystorePass -verbose -sigalg MD5withRSA -digestalg SHA1 -signedjar $unalignedPath $unsignedPath $keyAlias
jarsigner -verify -verbose -certs $unalignedPath

echo "Zipalign APK"
$ANDROID_HOME/build-tools/27.0.3/zipalign -f -v 4 $unalignedPath $apkPath