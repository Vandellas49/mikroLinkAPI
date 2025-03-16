# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Tüm projeleri kopyalayarak bağımlılıkları restore et
COPY ./mikroLinkAPI.Application/mikroLinkAPI.Application.csproj ./mikroLinkAPI.Application/
COPY ./mikroLinkAPI.Domain/mikroLinkAPI.Domain.csproj ./mikroLinkAPI.Domain/
COPY ./mikroLinkAPI.Infrastructure/mikroLinkAPI.Infrastructure.csproj ./mikroLinkAPI.Infrastructure/
COPY ./mikroLinkAPI.WebAPI/mikroLinkAPI.WebAPI.csproj ./mikroLinkAPI.WebAPI/

# Solution dosyasına göre restore işlemini yap
COPY ./mikroLinkAPI.sln ./ 
RUN dotnet restore ./mikroLinkAPI.sln

# Tüm kaynak kodunu kopyala
COPY . .

# WebAPI projesini build ve publish et
WORKDIR /app/mikroLinkAPI.WebAPI
RUN dotnet publish -c Release -o /out

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Yayınlanan dosyaları kopyala
COPY --from=build /out .

# Uygulamanın dinleyeceği port
EXPOSE 8080

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "mikroLinkAPI.WebAPI.dll"]

# Healthcheck ekle
HEALTHCHECK --interval=30s --timeout=10s --retries=3 \
  CMD curl --silent --fail http://localhost:8080/health || exit 1
