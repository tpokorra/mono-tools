//
// Gendarme.Framework.Rocks.PropertyRocks
//
// Authors:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// Copyright (C) 2011 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;

using Mono.Cecil;

namespace Gendarme.Framework.Rocks {

	// add Property[Reference|Definition][Collection] extensions methods here
	// only if:
	// * you supply minimal documentation for them (xml)
	// * you supply unit tests for them
	// * they are required somewhere to simplify, even indirectly, the rules
	//   (i.e. don't bloat the framework in case of x, y or z in the future)

	/// <summary>
	/// PropertyRocks contains extensions methods for Property[Definition|Reference]
	/// and the related collection classes.
	/// </summary>
	public static class PropertyRocks {

		/// <summary>
		/// Check if the property, or it's declaring type, was generated by the compiler or a tool (i.e. not by the 
		/// developer). This is especially important since automatic properties getter and setter methods are marked
		/// as generated code (because the compiler generates them with a backing field) while the property itself is
		/// not (because it comes from the developer source code).
		/// </summary>
		/// <param name="self">The PropertyDefinition on which the extension method can be called.</param>
		/// <returns>True if the code is not generated directly by the developer, 
		/// False otherwise (e.g. compiler or tool generated)</returns>
		public static bool IsGeneratedCode (this PropertyDefinition self)
		{
			// note: unlike other Cecil types there is no Resolve in PropertyReference
			if (self == null)
				return false;

			if (self.HasCustomAttributes) {
				if (self.CustomAttributes.ContainsAnyType (CustomAttributeRocks.GeneratedCodeAttributes))
					return true;
			}
			return self.DeclaringType.IsGeneratedCode ();
		}
	}
}
