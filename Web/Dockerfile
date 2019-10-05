FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build-sass-core

RUN apt-get update && apt-get install -y --no-install-recommends \
		sassc \
	&& rm -rf /var/lib/apt/lists/*
	
WORKDIR /app

COPY Web.csproj ./
RUN dotnet restore

COPY . ./
RUN sassc sass/site.scss wwwroot/gen/site.css
RUN dotnet publish -c Release -o pub

FROM mcr.microsoft.com/dotnet/core/apsnet:3.0-buster
WORKDIR /app
COPY --from=build-sass-core /app/pub .
ENTRYPOINT ["dotnet", "t3mhun.WebLog.Web.dll"]