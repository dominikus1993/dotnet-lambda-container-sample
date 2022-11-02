
FROM public.ecr.aws/lambda/dotnet:6 AS base

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim as build
WORKDIR /src
COPY . .
RUN dotnet tool restore
ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet paket restore

WORKDIR /src/src/dotnet-lambda-container-sample
RUN dotnet restore -nowarn:msb3202,nu1503 "dotnet-lambda-container-sample.csproj"
RUN dotnet build -c Release --output /app/build


FROM build AS publish
RUN dotnet publish "dotnet-lambda-container-sample.csproj" \
            --configuration Release \
            --runtime linux-x64 \
            --self-contained false \
            --output /app/publish \
            -p:PublishReadyToRun=true  

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .

CMD [ "dotnet-lambda-container-sample::dotnet_lambda_container_sample.Function::FunctionHandler"]