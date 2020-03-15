using System;
using System.Collections.Generic;
using Family;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace FamilyTest
{
    public class PersonTest
    {
        private readonly Person _ivan, _maria;

        public PersonTest()
        {
            _ivan = PersonData.Man;
            _maria = PersonData.Woman;
        }

        [Fact]
        public void IsAdult() 
            => (_ivan.IsAdult, _maria.IsAdult).Should().Be((true, false));

        [Fact]
        public void IsOrphan() 
        {
            var childWithNoParents = PersonData.GetBoy(parents: null);
            childWithNoParents.IsOrphan.Should().BeTrue();
            _ivan.IsOrphan.Should().BeFalse();
        }   
        
        [Fact]
        public void IsEqual()
        {
            (_ivan.Equals(PersonData.Man), _ivan.Equals(PersonData.Woman))
            .Should().Be((true, false));
        }

        [Fact]
        public void IsNotEqualToNull()
        {
            _ivan.Equals(null).Should().BeFalse();
        }
    }
}
