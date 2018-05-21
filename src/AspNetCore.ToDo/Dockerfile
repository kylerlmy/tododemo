From microsoft/donet:latest
COPY . /app
WORKDIR /app
RUN ["dotnet","resore"]
RUN ["dotnet","build"]
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet","run"]