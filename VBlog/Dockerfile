FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["VBlog/VBlog.csproj", "VBlog/"]
RUN dotnet restore "VBlog/VBlog.csproj"
COPY . .
WORKDIR "/src/VBlog"
RUN dotnet build "VBlog.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VBlog.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VBlog.dll"]