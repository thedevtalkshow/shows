# Terraform Start

## Prerequisites

- az cli
- azure subscription
- terraform cli

### Installing Terraform
- [Quickstart: Install and Configure Terraform](https://learn.microsoft.com/en-us/azure/developer/terraform/quickstart-configure)

I chose to install in WSL 2. I had Ubuntu 20.04 installed.

#### Updating the Azure CLI
Incidentally, the Ubuntu image on WSL had the Azure CLI (az) on it, but it was very old.  This matched what the Azure CLI documentation said about an azure cli package being preinstalled in Ubuntu, and I don't recall if I installed the Azure CLI myself in the past.

No matter, I went ahead and followed these instructions to remove the az cli on Ubuntu (in WSL 2) and install the latest.

- [Azure CLI Docs: Install the Azure CLI on Linux (Ubuntu/apt option)](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-linux?pivots=apt#before-you-begin)


I happened to use the install script: [Install Azure CLI - Option 1: Install with one command](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-linux?pivots=apt#option-1-install-with-one-command)

#### Finally, installing Terraform CLI
These following instructions use Git Bash to install.  As you can tell, I didn't want to do that.  These are here for reference, but skip below to what I did.

- [Azure Docs: Install Terraform on Windows with Bash](https://learn.microsoft.com/en-us/azure/developer/terraform/get-started-windows-bash?tabs=bash)

That said, I used the instructions on Hashicorp's Terraform for Linux to install on Ubuntu on WSL 2:
- [Terraform Docs: Install Terraform](https://learn.microsoft.com/en-us/azure/developer/terraform/get-started-windows-bash?tabs=bash)

#### Creating a service principal for Terraform

```
az ad sp create-for-rbac --name TerraformHandler --role Contributor --scopes /subscriptions/<subscription id>
```

You get this readout back... note the password for use as client secret:
```
Creating 'Contributor' role assignment under scope '/subscriptions/<real-subscription-id>'
The output includes credentials that you must protect. Be sure that you do not include these credentials in your code or check the credentials into your source control. For more information, see https://aka.ms/azadsp-cli
{
  "appId": "<real-appId>",
  "displayName": "TerraformHandler",
  "password": "<real-password/client secret>",
  "tenant": "<real-tenant-id>"
}
```

#### Added environment variables to .bashrc and reloaded environment

```
export ARM_SUBSCRIPTION_ID=""
export ARM_TENANT_ID=""
export ARM_CLIENT_ID=""
export ARM_CLIENT_SECRET=""
source ~/.bashrc
```

#### Aside: Created a symlink to source on C:
```
ln -s /mnt/c/Source ~
```