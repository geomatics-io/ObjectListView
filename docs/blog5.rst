.. -*- coding: UTF-8 -*-

:Subtitle: Lifting up the downtrodden ListViewGroup

.. _blog-listviewgroups:

Promoting ListView Groups
=========================

2012-04-20

`ListViewGroups` are strange, second class citizens in the ListView world.

They exist, but there is almost nothing you can do with them -- you can't
draw them yourself, you can't change their colours, you can't tell when
the user clicks on them (apart from the Group Task text). You can't even
find out when the user does something to them (selects, expands or collapses),
let alone stop them from doing it.

Naturally, I find all these limitations annoying. So, I wanted to see how
many of these limitations I could remove.

Knowing when a group is expanded/collapsed
------------------------------------------

Several people asked if there was a way to know when a group was expanded or collapsed.

There is no documented way to do this. But `this blog`_ shows that there is an
undocumented notification -- `LVN_FIRST - 88` --  whenever the state of a group changes
-- including when its 'collapsed-ness' changes. The notification block looks
like this::

    [StructLayout(LayoutKind.Sequential)]
    public struct NMLVGROUP
    {
        public NMHDR hdr;
        public int iGroupId; // which group is changing
        public uint uNewState; // LVGS_xxx flags
        public uint uOldState;
    }

.. _this blog: http://something.com

Combine this with the notification with the data block and we can start work.
In our method to handle ReflectNotify, we add a switch value of `LVN_FIRST - 88`
and in response to that notification, we do this::

    protected virtual bool HandleGroupInfo(ref Message m)
    {
        NativeMethods.NMLVGROUP nmlvgroup = (NativeMethods.NMLVGROUP)m.GetLParam(typeof(NativeMethods.NMLVGROUP));

        // Ignore state changes that aren't related to selection, focus or collapsedness
        const uint INTERESTING_STATES = (uint) (GroupState.LVGS_COLLAPSED | GroupState.LVGS_FOCUSED | GroupState.LVGS_SELECTED);
        if ((nmlvgroup.uOldState & INTERESTING_STATES) == (nmlvgroup.uNewState & INTERESTING_STATES))
            return false;

        foreach (OLVGroup group in this.OLVGroups) {
            if (group.GroupId == nmlvgroup.iGroupId) {
                GroupStateChangedEventArgs args = new GroupStateChangedEventArgs(group, (GroupState)nmlvgroup.uOldState, (GroupState)nmlvgroup.uNewState);
                this.OnGroupStateChanged(args);
                break;
            }
        }

        return false;
    }

Nothing very difficult here. We're only interested in selection, focus or collapsedness state changes.
If the state change is interesting, find the group and trigger an event.

This is better than nothing -- but not very much. We know that the state changed, but we can't stop the state
changing. We can't stop the group being selected, and most importantly, we can't stop the group being
expanded or collapsed.

I decided to come back to this problem later.

Did something hit the group?
----------------------------

Another fundamental ability that `ListViewGroups` lack is hit detection. There is no way to know
if the mouse is over a group. .NET's `ListView` doesn't include groups in its hit detection.

OK, let's drop down to the Windows SDK again. From Vista onwards, the `LVM_SUBITEMHITTEST` allows
some form of hit detection on groups.
The `iGroup` member is supposed to be the index of the group under the point::

    struct _LVHITTESTINFO {
        POINT pt;
        UINT flags;
        int iItem;
        int iSubItem;
        int iGroup;
    } LVHITTESTINFO;

We can use this to do this::

    NativeMethods.LVHITTESTINFO lParam = new NativeMethods.LVHITTESTINFO();
    lParam.pt_x = x;
    lParam.pt_y = y;
    int index = NativeMethods.HitTest(this, ref lParam);

When the point is over a group, `flags` has the value `LVHT_EX_GROUP_HEADER` but the `iGroup` is... always 0 :(

Hmmm... what's going on here? Digging a little more, the SDK says that this field is only filled in
when the listview is owner data (i.e. virtual). So, trying this hit test on a `FastObjectListView`
shows that the `iGroup` is reliable. In fact, it is always filled in if the virtual list is showing groups,
even when the hit point is on a list item, not a group itself.

But what about on a normal `ObjectListView`? After some more work, it turns out that
when a group is hit on a normal `ListView`, the value returned is the *id* (not the *index* as with
a virtual list) of the group. Putting all these variations together gives us code like this::

    // Figure out which group is involved in the hit test. This is a little complicated:
    // If the list is virtual:
    //   - the returned value is list view item index
    //   - iGroup is the *index* of the hit group.
    // If the list is not virtual:
    //   - iGroup is always -1.
    //   - if the point is over a group, the returned value is the *id* of the hit group.
    //   - if the point is not over a group, the returned value is list view item index.
    OLVGroup group = null;
    if (this.ShowGroups && this.OLVGroups != null) {
        if (this.VirtualMode) {
            group = lParam.iGroup >= 0 && lParam.iGroup < this.OLVGroups.Count ? this.OLVGroups[lParam.iGroup] : null;
        } else {
            bool isGroupHit = (lParam.flags & (int)HitTestLocationEx.LVHT_EX_GROUP) != 0;
            if (isGroupHit) {
                foreach (OLVGroup olvGroup in this.OLVGroups) {
                    if (olvGroup.GroupId == index) {
                        group = olvGroup;
                        break;
                    }
                }
            }
        }
    }

Cancelling state change
-----------------------

This good -- but not very helpful.

