﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IkitMita.DataAccess
{
    public class DomainObject : IDomainObject, IEquatable<IDomainObject>
    {
        private static int _idCounter;
        private int _id;

        public DomainObject()
        {
            if (_idCounter < (int.MinValue + 100))
            {
                _idCounter = 0;
            }

            _idCounter--;
            _id = _idCounter;
        }

        [Required]
        [Key]
        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual bool IsNew => Id < 1;

        public override bool Equals(object obj)
        {
            return Equals(obj as IDomainObject);
        }

        public virtual bool Equals(IDomainObject other)
        {
            return other != null && other.GetType() == GetType() && Id == other.Id;
        }

        public override int GetHashCode()
        {
            string hashString = string.Concat(GetType().FullName, "_", Id.ToString());
            return hashString.GetHashCode();
        }
    }
}
