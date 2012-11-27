using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Beerman006.TimeTracker.Modeling
{
    [Export(typeof(IClientManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class XMLClientManager : IClientManager
    {
        #region Fields
        /// <summary>
        /// Our clients.
        /// </summary>
        private readonly List<Client> _clients = new List<Client>();
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="XMLClientManager"/>.
        /// </summary>
        public XMLClientManager()
        {
            var client = new Client("Hasbro", "HB");
            client.AddWorkType("Federal");
            _clients.Add(client);

            client = new Client("Bank of America", "BOA");
            client.AddWorkType("Federal");
            client.AddWorkType("State", "BOAS");
            _clients.Add(client);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Client"/>s we're managing.
        /// </summary>
        public IEnumerable<Client> Clients { get { return _clients; } }

        /// <summary>
        /// Index this collection of clients to get a particular client by name.
        /// </summary>
        /// <param name="name">The name of the client to get.</param>
        /// <returns>The requested client, or <c>null</c> if that client does not exist.</returns>
        public Client this[string name]
        {
            get { return _clients.FirstOrDefault(c => c.Name == name); }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a client to the collection.
        /// </summary>
        /// <param name="client">The client to be added.</param>
        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        /// <summary>
        /// Remove a client from the collection.
        /// </summary>
        /// <param name="client">The client to be removed.</param>
        public void RemoveClient(Client client)
        {
            var clientToRemove = _clients.FirstOrDefault(c => c.Name == client.Name);
            if (clientToRemove != null)
            {
                _clients.Remove(clientToRemove);
            }
        }
        #endregion
    }
}
