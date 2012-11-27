using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beerman006.TimeTracker.Modeling
{
    /// <summary>
    /// An IClientManager is capable of managing clients - primarily persistance and management.
    /// </summary>
    public interface IClientManager
    {
        #region Properties
        /// <summary>
        /// Gets the available <see cref="Client"/>s.
        /// </summary>
        IEnumerable<Client> Clients { get; }

        /// <summary>
        /// Index this collection of clients to get a particular client by name.
        /// </summary>
        /// <param name="name">The name of the client to get.</param>
        /// <returns>The requested client, or <c>null</c> if that client does not exist.</returns>
        Client this[string name] { get;}
        #endregion

        #region Methods
        /// <summary>
        /// Add a client to the collection.
        /// </summary>
        /// <param name="client">The client to be added.</param>
        void AddClient(Client client);

        /// <summary>
        /// Remove a client from the collection.
        /// </summary>
        /// <param name="client">The client to be removed.</param>
        void RemoveClient(Client client);
        #endregion
    }
}
