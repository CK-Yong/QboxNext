dotnet publish QboxNext.Qserver.sln -c Release 

docker build ./QboxNext.Qserver/. -t "dotnetcore-minimal_qserver"
docker build ./QboxNext.Qservice/. -t "dotnetcore-minimal_qservice"

docker run -p 80:5000 -v qboxnextdata:/var/qboxnextdata dotnetcore-minimal_qserver 
docker run -p 5002:5002 -v qboxnextdata:/var/qboxnextdata dotnetcore-minimal_qservice 