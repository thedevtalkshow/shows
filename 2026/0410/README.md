# The Dev Talk Show April 10, 2026
## Structued Output with Agent Framework

In this episode, we will discuss how to use Agent Framework to create structured output.


### StructuredOutputBasic.Console

In this example, we will create a simple agent that generates structured output in the console. Instead of answering in the conversational tone of most chatbots, this agent will produce output in JSON.

```bash
dotnet new console -n StructuredOutputBasic.Console
cd StructuredOutputBasic.Console
```

Next, we will add the necessary NuGet packages for the Agent Framework.

```bash
dotnet add package Azure.AI.Projects --version 2.0.0
dotnet add package Microsoft.Agents.AI.Foundry --version 1.0.0
dotnet add package Azure.Identity --version 1.20.0
```





