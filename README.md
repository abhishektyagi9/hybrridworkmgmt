# Hybrid work management solution 

This reference architecture shows how to deploy an end-to-end hybrid work management application. It uses a  power apps app to book hybrid app and browser page for file/image for license plate ingestion, Azure Data Lake Storage to store the images, Azure Functions for serverless invocations, Azure Computer vision api (for this solution accelerator,) & CosmosDB to store the results.(PLACEHOLDER FOR POWERBI VISUALIZATION IF WE WANT). 

![](./_images/Architecture.jpg)

## Deploy

Before you hit the deploy button, make sure you review the details about the services deployed.

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fabhishektyagi9%2Fhybrridworkmgmt%2Fmaster%2FARMTemplates%2Fhybridworkemplatedeployment.parameters.json)

Once the resource deployed, you will need to deploy the functions to the Function App (at this time - could be further automated).

> **Important:** This deployment accelerator implements some service features that are still in Public Preview. Please consider those before you plan for a production deployment.
