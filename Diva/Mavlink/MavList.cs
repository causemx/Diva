using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Mavlink
{
	public class MavList : IEnumerable<MavStatus>, IDisposable
	{
		private Dictionary<int, MavStatus> masterlist = new Dictionary<int, MavStatus>();

		private Dictionary<int, MavStatus> hiddenlist = new Dictionary<int, MavStatus>();

		public MavlinkInterface parent;

		object locker = new object();

		public MavList(MavlinkInterface mavLinkInterface)
		{
			parent = mavLinkInterface;
			// add blank item
			hiddenlist.Add(0, new MavStatus(parent, 0, 0));
		}

		public void AddHiddenList(byte sysid, byte compid)
		{
			int id = GetID((byte)sysid, (byte)compid);

			hiddenlist[id] = new MavStatus(parent, sysid, compid);
		}

		public MavStatus this[int sysid, int compid]
		{
			get
			{
				int id = GetID((byte)sysid, (byte)compid);

				lock (locker)
				{
					// 3dr radio special case
					if (hiddenlist.ContainsKey(id))
						return hiddenlist[id];

					if (!masterlist.ContainsKey(id))
					{
						AddHiddenList((byte)sysid, (byte)compid);
						return hiddenlist[id];
					}

					return masterlist[id];
				}
			}
			set
			{
				int id = GetID((byte)sysid, (byte)compid);
				lock (locker)
				{
					masterlist[id] = value;
				}
			}
		}

		public int Count
		{
			get { return masterlist.Count; }
		}

		public List<int> GetRawIDS()
		{
			return masterlist.Keys.ToList<int>();
		}

		public void Clear()
		{
			masterlist.Clear();
		}

		public bool Contains(byte sysid, byte compid, bool includehidden = true)
		{
			lock (locker)
			{
				foreach (var item in masterlist.ToArray())
				{
					if (item.Value.sysid == sysid && item.Value.compid == compid)
						return true;
				}

				if (includehidden)
				{
					foreach (var item in hiddenlist.ToArray())
					{
						if (item.Value.sysid == sysid && item.Value.compid == compid)
							return true;
					}
				}
			}

			return false;
		}

		internal void Create(byte sysid, byte compid)
		{
			int id = GetID((byte)sysid, (byte)compid);

			// move from hidden to visible
			if (hiddenlist.ContainsKey(id))
			{
				masterlist[id] = hiddenlist[id];
				hiddenlist.Remove(id);
			}

			if (!masterlist.ContainsKey(id))
				masterlist[id] = new MavStatus(parent, sysid, compid);
		}

		public IEnumerator<MavStatus> GetEnumerator()
		{
			foreach (var key in masterlist.Values.ToArray())
			{
				yield return key;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public static int GetID(byte sysid, byte compid)
		{
			return sysid * 256 + compid;
		}

		public void Dispose()
		{
			foreach (var MAV in hiddenlist)
			{
				MAV.Value.Dispose();
			}

			foreach (var MAV in masterlist)
			{
				MAV.Value.Dispose();
			}
		}
	}
}
