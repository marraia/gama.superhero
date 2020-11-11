using Bogus;
using SuperHero.Application.AppHero.Input;
using SuperHero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHero.Tests.Comum
{
    internal class GenerateHeroFaker
    {
        public static List<Hero> CreateListHero(int qtd)
        {
            var hero = new Faker<Hero>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Editor, CreateEditor())
                .RuleFor(c => c.Age, f => f.Random.Int(1, 100))
                .RuleFor(c => c.Created, f => f.Date.Recent())
                .Generate(qtd);

            return hero;
        }

        
        public static Editor CreateEditor()
        {
            var editor = new Faker<Editor>("pt_BR")
                .StrictMode(true)
                .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.Name, f => f.Company.CompanyName())
                .Generate();

            return editor;
        }

        public static Hero CreateHero()
        {
            var hero = new Faker<Hero>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Editor, CreateEditor())
                .RuleFor(c => c.Age, f => f.Random.Int(1, 100))
                .RuleFor(c => c.Created, f => f.Date.Recent())
                .Generate();

            return hero;
        }

        public static Hero CreateHero(string name, int idEditor, int age)
        {
            var hero = new Faker<Hero>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Int(1, 10000))
                .RuleFor(c => c.Name, name)
                .RuleFor(c => c.Editor, new Editor(idEditor, "Marvel"))
                .RuleFor(c => c.Age, age)
                .RuleFor(c => c.Created, f => f.Date.Recent())
                .Generate();

            return hero;
        }

        public static HeroInput CreateHeroInput(int age)
        {
            var heroInput = new Faker<HeroInput>("pt_BR")
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.IdEditor, f =>f.Random.Int(1, 10000))
                .RuleFor(c => c.Age, age)
                .Generate();

            return heroInput;
        }
    }
}
