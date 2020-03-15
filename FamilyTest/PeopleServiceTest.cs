namespace FamilyTest
{
    using System;
    using System.Linq;
    using Family;
    using FluentAssertions;
    using Xunit;

    public class PeopleServiceTest 
    {
        private readonly IPeopleService _service = new PeopleService();

        // Given
        private readonly Person _ivan, _maria;

        private Person[] Parents => new[] { _ivan, _maria };

        public PeopleServiceTest()
        {
            //When
            _ivan = _service.Register<Man>(PersonData.Ivan.firstName, PersonData.Ivan.lastName, PersonData.Ivan.birthday, parents: null);
            _maria = _service.Register<Woman>(PersonData.Maria.firstName, PersonData.Maria.lastName, PersonData.Maria.birthday, parents: null);
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
            (_ivan.Spouse, _maria.Spouse).Should().Be((_maria, _ivan));
        }

        [Fact]
        public void ShouldNotMarryAlreadyMarried()
        {
            _service.Marry(_ivan, _maria);
            Action marryAction = () => _service.Marry(_maria, _ivan);
            marryAction.Should().Throw<InvalidOperationException>()
                                .WithMessage("*already married*");
        }

        [Fact]
        public void ShouldGetChildBorn()
        {
            var boy = _service.Register<Man>(
                PersonData.Petro.firstName,
                PersonData.Petro.lastName,
                PersonData.Petro.birthday,
                Parents);
                
            boy.Should().BeEquivalentTo(PersonData.GetBoy(null));
            boy.Parents.Should().BeEquivalentTo(Parents);

            (_ivan.Children.Single(c => c == boy), _maria.Children.Single(c => c == boy))
            .Should().Be((boy, boy));
        }

        [Fact]
        public void ShouldRegisterDeath()
        {
            _service.PassAway(_ivan, DateTime.Today);

            _ivan.HasGone.Should().BeTrue();
            _ivan.DeathDate.Should().Be(DateTime.Today);
        }

        [Fact]
        public void ChildShouldBecomeAnOrphant()
        {
            var boy = _service.Register<Man>(
                PersonData.Petro.firstName,
                PersonData.Petro.lastName,
                PersonData.Petro.birthday,
                Parents);
            boy.IsOrphan.Should().BeFalse();

            _service.PassAway(_ivan, DateTime.Today);
            _service.PassAway(_maria, at: DateTime.Today);

            boy.IsOrphan.Should().BeTrue();
        }

        [Fact]
        public void ShouldHaveChildren()
        {
            var boy = _service.Register<Man>(
                PersonData.Petro.firstName,
                PersonData.Petro.lastName,
                PersonData.Petro.birthday,
                Parents);
            var girl = _service.Register<Woman>(
                PersonData.Sveta.firstName,
                PersonData.Sveta.lastName,
                PersonData.Sveta.birthday,
                Parents);

            _ivan.Children.Should().HaveCount(2);
            _maria.Children.Should().HaveCount(2);
        }
    }
}