userName='danielcauser'
root="/Users/${userName}/Projects/ReleaseEverywhereBuildOnce/Scripts/drop"
packagePath="${root}/Release/com.sample.causerexception.apk"
# envName='Development'
envName='Test'
keystorePath="${root}/com.sample.causerexception.keystore"
keystoreAlias='com.sample.causerexception'
keystorePassword='123456'
minSdkVersion=19

# chmod +x test-release-apk.sh 
# chmod +x Change_Config_APK.sh 
# ./test-release-apk.sh 

SYSTEM_DEFAULTWORKINGDIRECTORY="${root}"

pushd ${root}
/Users/${userName}/Projects/ReleaseEverywhereBuildOnce/Scripts/drop/Scripts/Change_Config_APK.sh ${packagePath} "${envName}" ${keystorePath} ${keystoreAlias} ${keystorePassword} ${minSdkVersion}
popd
