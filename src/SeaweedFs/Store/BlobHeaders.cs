// ***********************************************************************
// Assembly         : SeaweedFs
// Author           : piechpatrick
// Created          : 10-11-2021
//
// Last Modified By : piechpatrick
// Last Modified On : 10-11-2021
// ***********************************************************************
using System.Collections.Generic;

namespace SeaweedFs.Store
{

    /// <summary>
    /// Class BlobHeaders.
    /// Implements the <see cref="System.Collections.Generic.Dictionary{System.String, System.Collections.Generic.IEnumerable{System.String}}" />
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{System.String, System.Collections.Generic.IEnumerable{System.String}}" />
    public class BlobHeaders : Dictionary<string, IEnumerable<string>>
    {
       
    }
}