namespace FamilyTest
{
    using System;
    using Family;
    using FluentAssertions;
    using Xunit;

    public class PeopleServiceTest 
    {
        private readonly IPeopleService _service = new PeopleService();
        
        private readonly Person _man, _woman;

        // Given
        private readonly (string firstName, string lastName, DateTime birthday) 
            _manData = ("Ivan", "Ivanov", DateTime.Today.AddYears(-20)),
            _womanData = ("Maria", "Petrova", DateTime.Today.AddYears(-17));

        public PeopleServiceTest()
        {
            //When
            _man = _service.Register<Man>(_manData.firstName, _manData.lastName, _manData.birthday);
            _woman = _service.Register<Woman>(_womanData.firstName, _womanData.lastName, _womanData.birthday);
        }

        [Fact]
        public void ShouldCreatePerson()
        {
            //Then
            (_man.FirstName, _man.LastName, _man.Birthday)
            .Should().Be(_manData);
            (_woman.FirstName, _woman.LastName, _woman.Birthday)
            .Should().Be(_womanData);
        }

        [Fact]
        public void ShouldDetectAdultPerson()
        {
            (_man.IsAdult, _woman.IsAdult).Should().Be((true, false));
        }

        [Fact]
        public void ShouldGetMarried()
        {
            // When
            _service.Marry(_man, _woman);
            (_man.IsMarried, _woman.IsMarried).Should().Be((true, true));
        }

        [Fact]
        public void ShouldNotMarryAlreadyMarried()
        {
            _service.Marry(_man, _woman);
            Action marryAction = () => _service.Marry(_woman, _man);
            marryAction.Should().Throw<InvalidOperationException>();
        }
    }
}