using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Beerman006.TimeTracker.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Occurs when properties in the class change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Sets a property and raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">The type of the property being set.</typeparam>
        /// <param name="propertyName">
        /// The name of the property being set.  For a typesafe, refactor friendly way to get the 
        /// property name, refer to <see cref="GetPropertyName{T}"/>
        /// </param>
        /// <param name="field">
        /// A reference to the field to be set.  This is the field that backs the property.
        /// </param>
        /// <param name="value">The new value for the property.</param>
        protected void SetProperty<T>(string propertyName, ref T field, T value)
        {
            field = value;
            NotifyPropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets the name of a property given an <see cref="Expression{Func{T}}"/> that refers to the property.
        /// </summary>
        /// <typeparam name="T">The type of the property for which to get a name.</typeparam>
        /// <param name="memberExpression">The expression refering to the property.</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentException">
        /// Throws an <see cref="ArgumentException"/> if the <see cref="Expression"/> does
        /// not refer to a property.
        /// </exception>
        protected string GetPropertyName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expression = memberExpression.Body as MemberExpression;
            if (expression == null || !(expression.Member is PropertyInfo))
            {
                throw new ArgumentException("memberExpression", "memberExpression must refer to a property");
            }
            return expression.Member.Name;
        }
        #endregion
    }
}
