# RolePlayEvalForLLM

A quick application built on top of my [LocalGenAI](https://github.com/joe-chiu/LocalGenAI) framework to evaluate how different local LLM models deal with the same set of role playing prompts.
The initial prompt was created for ChatGPT3.5 turbo, which it followed quite well. I then use the response from ChatGPT for few-shot learning for other models under evaluation.
Some models (say Llama2 13B) improved with the few shot learning examples from ChatGPT that they now better at following the format I asked for.
Some models don't really respond to the few shot learning examples.
