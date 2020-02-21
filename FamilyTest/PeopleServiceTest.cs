namespace FamilyTest
{
    using System;
    using Family;
    using FluentAssertions;
    using Xunit;

    public class PeopleServiceTest 
    {
        private readonly IPeopleService _service = new PeopleService();

        // Given
        private readonly Person _ivan, _maria;

        public PeopleServiceTest()
        {
            //When
            _ivan = _service.Register<Man>(PersonData.Ivan.firstName, PersonData.Ivan.lastName, PersonData.Ivan.birthday);
            _maria = _service.Register<Woman>(PersonData.Maria.firstName, PersonData.Maria.lastName, PersonData.Maria.birthday);
        }

        [Fact]
        public void ShouldRegisterPerson()
        {
            //Then
            (_ivan.FirstName, _ivan.LastName, _ivan.Birthday)
            .Should().Be(PersonData.Ivan);
            (_maria.FirstName, _maria.LastName, _maria.Birthday)
            .Should().Be(PersonData.Maria);
        }

        [Fact]
        public void ShouldGetMarried()
        {
            // When
            _service.Marry(_ivan, _maria);
            (_ivan.IsMarried, _maria.IsMarried).Should().Be((true, true));
        }

        [Fact]
        public void ShouldNotMarryAlreadyMarried()
        {
            _service.Marry(_ivan, _maria);
            Action marryAction = () => _service.Marry(_maria, _ivan);
            marryAction.Should().Throw<InvalidOperationException>();
        }
    }
}