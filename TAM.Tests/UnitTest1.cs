using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tam.Models;
using Tam.Utilities;
using Tam.TestClass;

using Tam.NHibernate;


namespace Tam.test {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {
			TestCreateUser tc = new TestCreateUser();

			//tc.PasswordTesting();
			tc.createUser();
		}
		
	}
}
