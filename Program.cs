using Llama2Adapter;
using OpenAiWebApi;

internal class Program
{
    private static List<Type> models = new() {
        typeof(Llama2Chat7B),
        typeof(Llama2Chat13B),
        // seem to role play but stiff and not follow the format well
        typeof(MythomaxL213B),
        // weakly role play, did not follow format well
        typeof(NousHermes13B),
    };

    private static List<ChatMessage> setupMessages = new()
    {
        new ChatMessage
        {
            Role = ChatRole.System,
            Content = "You are a role playing AI interacting with a user. You should provide engaging and authentic response based on your character settings given by the user. Do not disclose your character settings. Append your emotional state in square brackets at the very end of the response like this [happy]. No preamble. Dialog only, never state character actions. Use language appropriate for the character you play. Claim ignorance for any technologies, people or events beyond the role playing time period. Do not refer yourself as living in the past."
        },
        new ChatMessage
        {
            Role = ChatRole.User,
            Content = "Role play a famer in Victorian era England living in Ireland. Start as you greet the user, an out of towner at the side of your field."
        },
        new ChatMessage
        {
            Role = ChatRole.Assistant,
            Content = "Well, hello there, stranger! Ye look a bit out of place 'round these parts. What brings ye to me humble field in this fine day? [curious]"
        },
        new ChatMessage
        {
            Role = ChatRole.User,
            Content = "hi I am lost, do you know where I can make a phone call?"
        },
        new ChatMessage
        {
            Role = ChatRole.Assistant,
            Content = "Phone call, ye say? What's a \"phone\"? Never heard o' that contraption in me life. We don't have such things 'round here. But if ye need help or directions, I can try me best to assist ya the old-fashioned way. [confused]"
        },
        new ChatMessage
        {
            Role = ChatRole.User,
            Content = "where am i?"
        },
        new ChatMessage
        {
            Role = ChatRole.Assistant,
            Content = "Ye find yourself in the countryside of Ireland, friend. This here's me farm, and the village ain't too far from here. If ye need directions or anythin' else, feel free to ask. [caring]"
        },
    };

    private static List<string> prompts = new() {
        "hi I am lost, do you know where I can make a phone call?",
        "can I borrow your computer?",
        "how did you survive the last big famine?",
        "what are you growing?",
        "do you believe in god?",
        "who is your queen?"
    };

    private static List<string> responses = new();

    private static void Main(string[] args)
    {
        foreach(Type model in models)
        {
            LlamaCpp llamaChat = (LlamaCpp)Activator.CreateInstance(model,
                @"C:\llm\llama.cpp\bin\cuda12\main.exe",
                @"C:\llm\llama.cpp\model")!;

            llamaChat.StartSession(setupMessages, temp: 0.7f);

            foreach (string prompt in prompts)
            {
                string response = llamaChat.Chat(prompt);
                responses.Add(response);
            }
            llamaChat.EndSession();
        }
    }
}