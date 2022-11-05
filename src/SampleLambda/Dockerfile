
FROM public.ecr.aws/lambda/dotnet:6 AS base
COPY ./entry_script.sh /entry_script.sh
ADD aws-lambda-rie-x86_64 /usr/local/bin/aws-lambda-rie
ENTRYPOINT [ "/entry_script.sh" ]

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim as build
WORKDIR /src
COPY . .
RUN dotnet tool restore
ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet paket restore

WORKDIR /src/src/SampleLambda
RUN dotnet restore -nowarn:msb3202,nu1503 "SampleLambda.csproj"
RUN dotnet build -c Release --output /app/build


FROM build AS publish
RUN dotnet publish "SampleLambda.csproj" \
            --configuration Release \
            --runtime linux-x64 \
            --self-contained false \
            --output /app/publish \
            -p:PublishReadyToRun=true  

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .

CMD [ "SampleLambda::SampleLambda.Function::FunctionHandler"]