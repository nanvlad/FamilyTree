using System;
using System.Collections.Generic;
using Family;
using FluentAssertions;
using Xunit;

namespace FamilyTest
{
    public class PersonTest
    {
        private static readonly IEnumerable<Person> _noParents = Array.Empty<Person>();

        private readonly Person _mother;
        private readonly Person _child;
        private readonly Person _father;

        public PersonTest()
        {
            _mother = new Woman(GetBirthday(18), _noParents);
            _father = new Man(GetBirthday(17), _noParents);
            _child = new Man(GetBirthday(2), new[] { _mother, _father });
        }

        [Fact]
        public void ShouldBeAdult()
        {
            _mother.Age.Should().Be(18);
        }
        
        [Fact]
        public void IsOrphan()
        {
            _mother.IsOrphan.Should().BeFalse();
            _father.IsOrphan.Should().BeTrue();
        }

        [Fact]
        public void HasChild()
        {
            _mother.Children.Should().NotBeEmpty();
            _father.Children.Should().NotBeEmpty();
        }

        [Fact]
        public void HasParent()
        {
            _child.Parents.Should().NotBeEmpty();
        }

        [Fact]
        public void HasSibling()
        {
            var sister = new Woman(GetBirthday(2), new[] { _father });
            _child.Siblings.Should().HaveCount(1);
        }

        [Fact]
        public void BrotherHasSiblingButNotSister()
        {
            var sister = new Woman(GetBirthday(2), new[] { _father });
            _child.Siblings.Should().HaveCount(1);

            var brother = new Man(GetBirthday(3), new[] { _mother });
            _child.Siblings.Should().HaveCount(2);
            sister.Siblings.Should().HaveCount(1);
            brother.Siblings.Should().HaveCount(1);
        }

        private static DateTime GetBirthday(int years)
                => DateTime.Today.AddYears(-years);
    }
}
