using FluentAssertions;
using Marraia.Notifications;
using Marraia.Notifications.Handlers;
using Marraia.Notifications.Interfaces;
using Marraia.Notifications.Models;
using Marraia.Notifications.Models.Enum;
using MediatR;
using NSubstitute;
using SuperHero.Application.AppHero;
using SuperHero.Application.AppHero.Input;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Tests.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SuperHero.Tests.AppHero
{
    public class HeroAppServiceTests
    {
        const int Id = 10;
        private IHeroRepository subHeroRepository;
        private HeroAppService heroAppService;
        private SmartNotification smartNotification;
        private INotificationHandler<DomainNotification> subNotificationHandler;
        private DomainNotificationHandler domainNotificationHandler;

        public HeroAppServiceTests()
        {
            domainNotificationHandler = new DomainNotificationHandler();
            this.subNotificationHandler = domainNotificationHandler;
            this.subHeroRepository = Substitute.For<IHeroRepository>();
            this.smartNotification = new SmartNotification(this.subNotificationHandler);
            this.heroAppService = new HeroAppService(this.smartNotification, this.subHeroRepository);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(5)]

        public void Validar_Metodo_Get_Com_Ou_Sem_Dados(int qtd)
        {
            //Arrange
            var listHero = GenerateHeroFaker.CreateListHero(qtd);

            this.subHeroRepository
                .Get()
                .Returns(listHero);

            //Act
            var result = this.heroAppService.Get();

            //Assert
            result
                .Should()
                .BeOfType<List<Hero>>();

            result
                .Should()
                .HaveCount(qtd);

            this.subHeroRepository
                    .Received(1)
                    .Get();
        }


        [Fact]
        public async Task Validar_Metodo_GetById_Com_Dados()
        {
            //Arrange
            var hero = GenerateHeroFaker.CreateHero();

            this.subHeroRepository
                .GetByIdAsync(Id)
                .Returns(hero);

            //Act
            var result = await this.heroAppService
                                    .GetByIdAsync(Id)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .BeOfType<Hero>();

            result.Id.Should().NotBe(0);
            result.Name.Should().Be(hero.Name);
            result.Editor.Id.Should().Be(hero.Editor.Id);
            result.Editor.Name.Should().Be(hero.Editor.Name);
            result.Age.Should().Be(hero.Age);

            await this.subHeroRepository
                    .Received(1)
                    .GetByIdAsync(Arg.Any<int>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task Validar_Metodo_GetById_Sem_Dados()
        {
            //Arrange
            var hero = default(Hero);

            this.subHeroRepository
                .GetByIdAsync(Id)
                .Returns(hero);

            //Act
            var result = await this.heroAppService
                                    .GetByIdAsync(Id)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .BeNull();

            await this.subHeroRepository
                    .Received(1)
                    .GetByIdAsync(Arg.Any<int>())
                    .ConfigureAwait(false);
        }

        [Theory]
        [InlineData("", 0, 0)]
        [InlineData("Fernando", 1, 0)]
        [InlineData("", 1, 1)]
        [InlineData("Fernando", 0, 0)]
        public async Task Validar_Metodo_Insert_Sem_Dados_Obrigatorios(string name, int idEditor, int age)
        {
            //Arrange
            var input = new HeroInput();
            input.Name = name;
            input.IdEditor = idEditor;
            input.Age = age;

            //Act
            var result = await this.heroAppService
                                    .InsertAsync(input)
                                    .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .Be(default(Hero));

            domainNotificationHandler
                .GetNotifications()
                .Should()
                .HaveCount(1);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .DomainNotificationType
                .Should()
                .Be(DomainNotificationType.BadRequest);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .Value
                .Should()
                .Be("Os dados são obrigatórios");
        }

        [Theory]
        [InlineData(17)]
        [InlineData(5)]
        [InlineData(1)]
        public async Task Validar_Metodo_Insert_Hero_Menor_de_Idade(int age)
        {
            //Arrange
            var input = GenerateHeroFaker.CreateHeroInput(age);

            //Act
            var result = await this.heroAppService
                        .InsertAsync(input)
                        .ConfigureAwait(false);

            //Assert
            result
                .Should()
                .Be(default(Hero));

            domainNotificationHandler
                .GetNotifications()
                .Should()
                .HaveCount(1);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .DomainNotificationType
                .Should()
                .Be(DomainNotificationType.Conflict);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .Value
                .Should()
                .Be("O heroi não é maior de idade");
        }

        [Theory]
        [InlineData(18)]
        [InlineData(100)]
        [InlineData(50)]
        public async Task Validar_Metodo_Insert_Com_Sucesso(int age)
        {
            //Arrange
            var input = GenerateHeroFaker.CreateHeroInput(age);
            var hero = GenerateHeroFaker.CreateHero(input.Name, input.IdEditor, input.Age);

            this.subHeroRepository
                .GetByIdAsync(Arg.Any<int>())
                .Returns(hero);

            this.subHeroRepository
                .InsertAsync(hero)
                .Returns(hero.Id);

            //Act
            var result = await this.heroAppService
                                    .InsertAsync(input)
                                    .ConfigureAwait(false);


            //Assert
            result
                .Should()
                .BeOfType<Hero>();

            result.Id.Should().Be(hero.Id);
            result.Name.Should().Be(hero.Name);

            await this.subHeroRepository
                        .Received(1)
                        .GetByIdAsync(Arg.Any<int>())
                        .ConfigureAwait(false);

            await this.subHeroRepository
                    .Received(1)
                    .InsertAsync(Arg.Any<Hero>())
                    .ConfigureAwait(false);
        }


    }
}
