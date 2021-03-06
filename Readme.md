# Speech Verification

The goal of this project is to demonstrate, how Azure Speech Recognition can be used to create password less login, with help of voice fingerprints.

## Disclaimer

This project is just to demonstrate how voice login can be implemented, but it is not meant to encourage implementation of any voice login in production yet.

Speaker Recognition in Azure is still in preview. There might be changes to the API in the future.

## Prerequisites

- Free [Azure](https://azure.microsoft.com/) account
- Free [Speaker Recognition (preview)](https://azure.microsoft.com/en-us/services/cognitive-services/speaker-recognition/) account
- [.NET Core](https://dotnet.microsoft.com/) 2.1+ _created on .NET Core 3.1 preview_
- [`ffmpeg`](https://www.ffmpeg.org/)
- [User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

## Structure

### `SpeechVerificationServices`

This project is just a wrapper/client for easy consumption of `Speaker Recognition API`.

### `SpeechVerification`

This project is an example implementation of `SpeechVerificationServices` in an ASP.NET Core Web app.

SpeechVerification uses `MediaStream API` to record sound bits from a browser and `ffmpeg` to convert the sound file to 16-bit 16K mono PCM (as required by `Speaker Recognition API`).

In this project, I have left out integrating with authentication and identity of ASP.NET Core.

#### User Secrets

In .NET Core you can save and access the secretes (i.e. API keys) in a secure manner. It also prevents you from pushing the keys to a repository.

**NOTE:** By mistake, I did manage to push my API key to the GitHub. The key in question has been invalidated and new key resides in User Secrets.

To add your own key to the User Secrets:

```bash
dotnet user-secrets set "SpeechRecognitionAPI:ServiceApiKey" "<YOUR-API-KEY>" 
```
Make sure your working directory is `SpeechVerification` project directory, before you add the key to the user secrets store.
