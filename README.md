# Unimatrix .NET SDK

[![NuGet](https://img.shields.io/nuget/v/UniSdk.svg)](https://www.nuget.org/packages/UniSdk/) [![Release](https://img.shields.io/github/release/unimtx/uni-dotnet-sdk.svg)](https://github.com/unimtx/uni-dotnet-sdk/releases/latest) [![GitHub license](https://img.shields.io/badge/license-MIT-brightgreen.svg)](https://github.com/unimtx/uni-dotnet-sdk/blob/main/LICENSE)

The Unimatrix .NET SDK provides convenient access to integrate communication capabilities into your .NET applications using the Unimatrix HTTP API. The SDK provides support for sending SMS, 2FA verification, and phone number lookup.

## Getting started

Before you begin, you need an [Unimatrix](https://www.unimtx.com/) account. If you don't have one yet, you can [sign up](https://www.unimtx.com/signup?s=dotnet.sdk.gh) for an Unimatrix account and get free credits to get you started.

## Documentation

Check out the documentation at [unimtx.com/docs](https://www.unimtx.com/docs) for a quick overview.

## Installation

The recommended way to install the Unimatrix SDK for .NET is to use the nuget package manager, which is available on [NuGet](https://www.nuget.org/packages/UniSdk/).

If you are building with the .NET CLI, run the following command to add `UniSdk` as a dependency to your project:

```bash
dotnet add package UniSdk
```

If you are using the Visual Studio IDE, run the following command in the Package Manager Console:

```dotnet
Install-Package UniSdk
```

## Usage

The following example shows how to use the Unimatrix .NET SDK to interact with Unimatrix services.

### Send SMS

Send a text message to a single recipient.

```cs

using System;
using UniSdk;

var client = new UniClient("your access key id", "your access key secret");

try
{
    var resp = client.Messages.Send(new {
        to = "your phone number",
        signature = "your sender name",
        content = "Your verification code is 2048."
    });
    Console.WriteLine(resp.Data);
} catch (UniException ex)
{
    Console.WriteLine(ex);
}

```

## Reference

### Other Unimatrix SDKs

To find Unimatrix SDKs in other programming languages, check out the list below:

- [Java](https://github.com/unimtx/uni-java-sdk)
- [Go](https://github.com/unimtx/uni-go-sdk)
- [Node.js](https://github.com/unimtx/uni-node-sdk)
- [Python](https://github.com/unimtx/uni-python-sdk)
- [PHP](https://github.com/unimtx/uni-php-sdk)
- [Ruby](https://github.com/unimtx/uni-ruby-sdk)

## License

This library is released under the [MIT License](https://github.com/unimtx/uni-dotnet-sdk/blob/main/LICENSE).
