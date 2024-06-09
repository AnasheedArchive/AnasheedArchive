# AnasheedArchive

A website for archiving, documenting and translating Anasheed.

# Features

- [x] Full transcription and translations of Anasheed
- [ ] Subtitle files to be used in videos
- [ ] Search functionality

# Development

The project is made using blazor so all you need is .NET 8

Clone the repo

```shell
$ git clone --recurse-submodules --remote-submodules https://github.com/AnasheedArchive/Site.git
```

Download dependencies and run

```shell
$ dotnet restore --project AnasheedArchive/AnasheedArchive.csproj
$ dotnet run --project AnasheedArchive/AnasheedArchive.csproj
```

After running the site you'll find that the Anasheed page is empty, so create some mock files in `AnasheedArchive/Content/Anasheed` or download some from the site.

running the site will also generate a folder that contains vanilla html / css / js in `AnasheedArchive/output` that's used for hosting.

# Contributing

Currently looking for someone to help me with the site's design, but all contributions are welcome.

If you want to contribute to the anasheed collection then go to the [Anasheed repo](https://github.com/AnasheedArchive/Anasheed)

# FAQ

*better FAQ can be found [here](<AnasheedArchive/Content/about.md>)*

#### Q: what is the purpose of this project?
- A: this project is meant to archive a type of islamic media known as "Anasheed" (plural of "Nasheed") 

#### Q: what is a "Nasheed"?
- A: a Nasheed (chant) is a type of vocal music without instruments (read more [here](https://en.wikipedia.org/wiki/Nasheed))

#### Q: Copyright?
- A: Copyleft! everything is open source, do whatever you want with it, but credit me please :<
