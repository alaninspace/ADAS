#!/bin/bash

# Ensure the local .NET 10 SDK is used
export PATH="/Users/Alan/.dotnet:$PATH"

# Enable development mode to load user-secrets
export ASPNETCORE_ENVIRONMENT=Development

echo "Starting the ADAS Full Stack (API Backend + Blazor WASM Frontend)..."
echo "The API serves the WebAssembly frontend directly."
echo "Press Ctrl+C to stop."
echo "-------------------------------------------------------------------"

cd Adas.Api
dotnet run
