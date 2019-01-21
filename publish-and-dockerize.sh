dotnet publish QboxNext.Qserver.sln -c Release 

docker build ./QboxNext.Qserver/. -t "dotnetcore-minimal_qserver"
docker build ./QboxNext.Qservice/. -t "dotnetcore-minimal_qservice"

docker run dotnetcore-minimal_qserver -p 80:5000 -v qboxnextdata:/var/qboxnextdata
docker run dotnetcore-minimal_qservice -p 5001:5001 -v qboxnextdata:/var/qboxnextdata