namespace ActiveUp.Net.Mail.PDI.vCalendar
{
    /// <summary>
    /// Description résumée de vCard.
    /// </summary>
    [System.Serializable]
    public class Address
    {
        public Address()
        {
            //
            // TODO : ajoutez ici la logique du constructeur
            //
        }

        public Address(string email)
        {
            this._email = email;
        }
        private string _name, _email;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }
      
    }
}
