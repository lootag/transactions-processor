FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app
COPY ./Lootag.TransactionsProcessor.WebApi Lootag.TransactionsProcessor.WebApi
COPY ./Lootag.TransactionsProcessor.Domain Lootag.TransactionsProcessor.Domain
COPY ./Lootag.TransactionsProcessor.Services Lootag.TransactionsProcessor.Services
COPY ./transactions-processor.sln transactions-processor.sln
RUN dotnet publish -c Release -o target

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=build-env /app/target .

ENTRYPOINT [ "dotnet", "Lootag.TransactionsProcessor.WebApi.dll" ]
