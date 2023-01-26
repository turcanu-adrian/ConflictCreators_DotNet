using Domain.Games.Elements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var promptSet = new PromptSet
            {
                CreatedByUserId = "default",
                Name = "Default Prompt Set",
                Tags = new[] { "default", "basic" }
            };

            Prompt[] prompts = new[]
            {
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What color is the sun?",
                    CorrectAnswer = "Yellow",
                    WrongAnswers = new[] { "Green", "Red", "Blue", "Orange" }
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "How many legs does a sheep have?",
                    CorrectAnswer = "4",
                    WrongAnswers = new[] { "2", "3", "4" }
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What is the capital of France?",
                    CorrectAnswer = "Paris",
                    WrongAnswers = new [] { "Bucharest", "Rome", "Moscow"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What month has the internship ended in?",
                    CorrectAnswer = "January",
                    WrongAnswers = new[] { "December", "October", "February" }
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What is the capital of Romania?",
                    CorrectAnswer = "Bucharest",
                    WrongAnswers = new[] { "Paris", "Rome", "Texas" }
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "How many grams makes 1 kilogram?",
                    CorrectAnswer = "1000 grams",
                    WrongAnswers = new[] { "10 grams", "100 grams", "1 gram" }
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "How many states are in the United States?",
                    CorrectAnswer = "50",
                    WrongAnswers = new [] { "40", "45", "52"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What is the National Animal of the USA?",
                    CorrectAnswer = "Bald Eagle",
                    WrongAnswers = new[] { "Black Bear", "Grizzly Bear", "American Bison"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "Which type of fish is Nemo?",
                    CorrectAnswer = "Clownfish",
                    WrongAnswers = new[] { "Swordfish", "Sailfish", "Whale" }
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "Which is the largest continent?",
                    CorrectAnswer = "Asia",
                    WrongAnswers = new [] { "Europe", "North America", "Africa"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What is the smallest breed of dog?",
                    CorrectAnswer = "Chiuaua",
                    WrongAnswers = new [] { "Golden Retriever", "Bulldog", "Pitbull"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "How many sides does an Octagon have?",
                    CorrectAnswer = "8",
                    WrongAnswers = new[] { "4", "6", "10"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "Which bird can mimic humans?",
                    CorrectAnswer = "Parrot",
                    WrongAnswers = new [] { "Eagle", "Seagull", "Pelican"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What do bees produce?",
                    CorrectAnswer = "Honey",
                    WrongAnswers = new [] { "Milk", "Flowers", "Water"}
                },
                new Prompt
                {
                    PromptSetId = promptSet.Id,
                    Question = "What color is an emerald?",
                    CorrectAnswer = "Green",
                    WrongAnswers = new [] { "Golden", "Black", "Pink"}
                }
            };



            modelBuilder.Entity<PromptSet>()
                .HasData(promptSet);

            modelBuilder.Entity<Prompt>()
                .HasData(prompts);
        }
    }
}
