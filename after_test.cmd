nuget install NUnit.Console -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
cd C:\projects\swagattack-gameserver-mirror\tools
REM nunit3-console C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\bin\Debug\netcoreapp2.0\WebserverUnitTests.dll --result=myresults.xml;format=AppVeyor
REM OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"dotnet.exe" -oldStyle -register:user -targetargs:"test C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\WebserverUnitTests.csproj --test-adapter-path:. --logger:Appveyor" -filter:"+[*]* -[*.Tests]*"
dotnet test C:\projects\swagattack-gameserver-mirror\GameServer\Application.Test.Unittests\Application.Test.Unittests.csproj --no-build --no-restore --logger:Appveyor --test-adapter-path:. /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=\"%userprofile%\.nuget\packages\**\**\*.*,%userprofile%\.nuget\packages\**\*.*,%userprofile%\.nuget\packages\**\**\**\*.*,D:/repos/nunit/nunit3-vs-adapter/src/NUnitTestAdapter/*.cs,D:/**/**/**/**/**/*.cs,D:/**/**/**/**/**/**/*.cs,..\SwagAttack_Gameserver_Mirror\GameServer\ITTestCore\bin\Debug\NUnit3.*,D:/*.*,D:/**/*.*,\"
dotnet test C:\projects\swagattack-gameserver-mirror\GameServer\CommunicationUnitTest\Communication.Test.Unittests.csproj --no-build --no-restore --logger:Appveyor --test-adapter-path:. /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=\"%userprofile%\.nuget\packages\**\**\*.*,%userprofile%\.nuget\packages\**\*.*,%userprofile%\.nuget\packages\**\**\**\*.*,D:/repos/nunit/nunit3-vs-adapter/src/NUnitTestAdapter/*.cs,D:/**/**/**/**/**/*.cs,D:/**/**/**/**/**/**/*.cs,..\SwagAttack_Gameserver_Mirror\GameServer\ITTestCore\bin\Debug\NUnit3.*,D:/*.*,D:/**/*.*,\"
dotnet test C:\projects\swagattack-gameserver-mirror\GameServer\Domain.Test.Unittests\Domain.Test.Unittests.csproj --no-build --no-restore --logger:Appveyor --test-adapter-path:. /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=\"%userprofile%\.nuget\packages\**\**\*.*,%userprofile%\.nuget\packages\**\*.*,%userprofile%\.nuget\packages\**\**\**\*.*,D:/repos/nunit/nunit3-vs-adapter/src/NUnitTestAdapter/*.cs,D:/**/**/**/**/**/*.cs,D:/**/**/**/**/**/**/*.cs,..\SwagAttack_Gameserver_Mirror\GameServer\ITTestCore\bin\Debug\NUnit3.*,D:/*.*,D:/**/*.*,\"
coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i ..\GameServer\Application.Test.Unittests\coverage.xml --jobId %APPVEYOR_BUILD_NUMBER%-Application
coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i ..\GameServer\CommunicationUnitTest\coverage.xml --jobId %APPVEYOR_BUILD_NUMBER%-Communication
coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i ..\GameServer\Domain.Test.Unittests\coverage.xml --jobId %APPVEYOR_BUILD_NUMBER%-Domain
REM C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\WebserverUnitTests.csproj --no-build --no-restore