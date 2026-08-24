/*
 * DELIBERATE SERIALISATION FIXTURE. NOT FOR MERGE.
 *
 * Authored to validate CI_Toolkit PR #9 (findings-register item 4). ci-serialisation only
 * runs its baseline leg when the branch leg reports status == 'Error', and no sandbox repo
 * currently produces a serialisation failure, so the baseline path had never executed under
 * the fix. This type forces one.
 *
 * Why it fails: Helpers.ObjectTypesToTest() selects every non-abstract IObject whose every
 * property sits in a BH.* or System.* namespace, so System.IntPtr passes selection. The BHoM
 * serialiser has no BSON mapping for IntPtr, so ToJson() throws, Verify records an error, the
 * json comes back empty and ObjectToFromJson returns TestStatus.Error for this type.
 *
 * The failure is subject-side and exists only on this branch, so the baseline leg should not
 * see it and the comparator should report a regression. That is the intended outcome.
 */

using System;
using System.Collections.Generic;

using BH.oM.Base;

namespace BH.oM.XML.Item4Fixture
{
    public class UnserialisableFixture : IBHoMObject
    {
        // The whole fixture: IntPtr has no BSON serialiser.
        public virtual IntPtr Handle { get; set; } = IntPtr.Zero;

        public virtual Guid BHoM_Guid { get; set; } = Guid.NewGuid();
        public virtual Dictionary<string, object> CustomData { get; set; } = new Dictionary<string, object>();
        public virtual string Name { get; set; } = "";
        public virtual FragmentSet Fragments { get; set; } = new FragmentSet();
        public virtual HashSet<string> Tags { get; set; } = new HashSet<string>();
    }
}
