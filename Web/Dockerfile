FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

RUN apt-get update && apt-get install -y --no-install-recommends \
		sassc \
	&& rm -rf /var/lib/apt/lists/*
	
WORKDIR /app

COPY Web.csproj ./
RUN dotnet restore

COPY . ./
RUN mkdir wwwroot/gen
RUN sassc sass/site.scss wwwroot/gen/site.css
RUN dotnet publish -c Release -o pub

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/pub ./
ENTRYPOINT ["dotnet", "t3hmun.WebLog.Web.dll"]