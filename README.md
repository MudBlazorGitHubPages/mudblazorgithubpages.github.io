# MudBlazor GitHub Pages

[![](../../workflows/gh-pages/badge.svg)](../../actions)


This project is an example of using GitHub Actions to automatically deploy a .NET 9 [MudBlazor](https://mudblazor.com/) WebAssembly SPA (Single Page Application) to GitHub Pages. For a live demo, check the following link:

https://mudblazorgithubpages.github.io

Microsoft Docs already contains a [general overview](https://learn.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly?view=aspnetcore-8.0#github-pages)
of the steps needed for a successful deploy.

## How to deploy
# How to Fork
1. Fork this repository
2. Go to the repository you just created, then go to: `Settings` > `GitHub Pages` > set the source branch to `gh-pages-from-actions`
3. [Set permissions for GitHub Actions](https://stackoverflow.com/questions/73687176/permission-denied-to-github-actionsbot-the-requested-url-returned-error-403)
4. View your site at https://`your_user_name | your_organization_name`.github.io/`name_you_used_in_step1`
5. (optional) Enable Dependabot
    1. `Insights` > `Dependency graph` > `Dependabot` > Enable Dependabot 
    2. Create a branch named `dev` (branched from the `main` branch)

## How it works

The CI pipelines first perform a normal `dotnet publish` of the app, which will generate
a `dist` bundle ready to be deployed. This bundle is then pushed differently depending on
the CI environment:

- Azure Pipelines: the bundle is force pushed to `gh-pages` by using raw Git
commands
- GitHub Actions: an already existing [action](https://github.com/marketplace/actions/deploy-to-github-pages)
is used to push the bundle to `gh-pages-from-actions`

## How to use this for your own project

The `<base>` url in [`index.html`](src/Client/wwwroot/index.html) will need to be modified 
depending on where the project is deployed. If deploying on the root level, set 
`segmentCount = 0` in [`404.html`](src/Client/wwwroot/404.html) as well.

When testing on localhost, the `applicationUrl` for IIS Express in 
[`launchSettings.json`](src/Client/Properties/launchSettings.json) will need to be updated to 
reflect the same base url as in [`index.html`](src/Client/wwwroot/index.html).

Paths in the [Azure Pipelines yaml file](azure-pipelines.yml) / [GitHub Actions workflow](.github/workflows/gh-pages.yml)
may need to be updated accordingly.


## CI / CD

The Azure pipeline is expecting access to one variable group named `GitHubPATGroup`, which
should contain the following three variables:

- `GitHubPAT`: A Personal Access Token with sufficient permission to (force) push to the `gh-pages` branch
- `GitHubName`: The name of the user committing to the `gh-pages` branch
- `GitHubEmail`: The email of the user committing to the `gh-pages` branch

The `gh-pages` branch **must** exist already for the deployment to be successful (this
is a temporary limitation in the pipeline configuration).

In the case of GitHub Actions, only a single secret is needed: `ACCESS_TOKEN`, equivalent to `GitHubPAT` above. An example of a full deployment using GitHub Actions can also be found in my [blazor-fractals](https://github.com/fernandreu/blazor-fractals) repository.
