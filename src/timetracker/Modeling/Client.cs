using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Beerman006.TimeTracker.Modeling
{
    /// <summary>
    /// Represents a client to which time can be charged.
    /// </summary>
    public class Client
    {
        #region Fields
        /// <summary>
        /// The name of the client.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// The default charge code for the client.
        /// </summary>
        private string _defaultChargeCode;

        /// <summary>
        /// Maps a single charge code to the various work types that can be charged to that code.
        /// </summary>
        private readonly Dictionary<string, HashSet<string>> _codeToTypeMap = new Dictionary<string, HashSet<string>>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new <see cref="Client"/>.
        /// </summary>
        /// <param name="name">The name of the client.</param>
        /// <param name="defaultChargeCode">
        /// The default charge code for the client.  This can be <c>null</c> or empty.
        /// </param>
        public Client(string name, string defaultChargeCode)
        {
            _name = name;
            _defaultChargeCode = defaultChargeCode;
        }

        /// <summary>
        /// Constructs a new <see cref="Client"/>.
        /// </summary>
        /// <param name="name">The name of the client.</param>
        public Client(string name) : this(name, string.Empty) { }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the types of work that can be done for this client.
        /// </summary>
        public IEnumerable<string> WorkTypes { get { return _codeToTypeMap.Values.SelectMany(s => s); } }

        /// <summary>
        /// Gets the charge codes applicable to this client.
        /// </summary>
        public IEnumerable<string> ChargeCodes { get { return _codeToTypeMap.Keys; } }

        /// <summary>
        /// Gets the default charge code for the client.
        /// </summary>
        public string DefaultChargeCode
        {
            get { return _defaultChargeCode; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a type of work to the client.
        /// </summary>
        /// <param name="workType">The type of work to be added to the client.</param>
        /// <param name="chargeCode">The charge code that should be used for the type of work.</param>
        public void AddWorkType(string workType, string chargeCode)
        {
            ValidateWorkType(workType, chargeCode);
            if (!_codeToTypeMap.ContainsKey(chargeCode))
            {
                _codeToTypeMap[chargeCode] = new HashSet<string>();
            }
            _codeToTypeMap[chargeCode].Add(workType);
        }

        /// <summary>
        /// Add a type of work to the client.
        /// </summary>
        /// <param name="workType">The type of work to be added to the client.</param>
        /// <remarks>The work type is added under the <see cref="DefaultChargeCode"/>.</remarks>
        public void AddWorkType(string workType)
        {
            AddWorkType(workType, DefaultChargeCode);
        }

        /// <summary>
        /// Gets the charge code to be used for a specific work type.
        /// </summary>
        /// <param name="workType">The work type under consideration.</param>
        /// <returns>The charge code to be used for the given work type.</returns>
        public string GetChargeCodeFromWorkType(string workType)
        {
            if (string.IsNullOrEmpty(workType))
            {
                return DefaultChargeCode;
            }

            foreach (var kvp in _codeToTypeMap)
            {
                if (kvp.Value.Contains(workType))
                {
                    return kvp.Key;
                }
            }
            return DefaultChargeCode;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates that we're not adding a work type that is already associated with another charge code.
        /// </summary>
        /// <param name="workType">The new work type being added.</param>
        /// <param name="chargeCode">The charge code it is to be associated with.</param>
        private void ValidateWorkType(string workType, string chargeCode)
        {
            foreach (var kvp in _codeToTypeMap)
            {
                if (kvp.Value.Contains(workType) && kvp.Key != chargeCode)
                {
                    throw new ArgumentException("workType", string.Format("Work type {0} is already associated with a charge code", workType));
                }
            }
        }
        #endregion
    }
}
