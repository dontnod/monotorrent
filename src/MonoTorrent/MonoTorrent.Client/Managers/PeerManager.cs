//
// PeerManager.cs
//
// Authors:
//   Alan McGovern alan.mcgovern@gmail.com
//
// Copyright (C) 2006 Alan McGovern
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//


using System.Collections.Generic;

namespace MonoTorrent.Client
{
    public class PeerManager
    {
        internal List<PeerId> ConnectedPeers;
        internal List<PeerId> HandshakingPeers;
        internal List<Peer> ConnectingToPeers;

        internal List<Peer> ActivePeers;
        internal List<Peer> AvailablePeers;
        internal List<Peer> BannedPeers;
        internal List<Peer> BusyPeers;

        /// <summary>
        /// The number of peers which are available to be connected to.
        /// </summary>
        public int Available { get; private set; }

        /// <summary>
        /// Returns the number of Leechs we are currently connected to.
        /// </summary>
        /// <returns></returns>
        public int Leechs { get; private set; }

        /// <summary>
        /// Returns the number of Seeds we are currently connected to.
        /// </summary>
        /// <returns></returns>
        public int Seeds { get; private set; }

        internal PeerManager()
        {
            ConnectedPeers = new List<PeerId>();
            ConnectingToPeers = new List<Peer>();
            HandshakingPeers = new List<PeerId>();

            ActivePeers = new List<Peer>();
            AvailablePeers = new List<Peer>();
            BannedPeers = new List<Peer>();
            BusyPeers = new List<Peer>();
        }

        internal void ClearAll()
        {
            ConnectedPeers.Clear ();
            ConnectingToPeers.Clear ();
            HandshakingPeers.Clear ();

            ActivePeers.Clear ();
            AvailablePeers.Clear ();
            BannedPeers.Clear ();
            BusyPeers.Clear ();
        }

        internal bool Contains(Peer peer)
        {
            return ActivePeers.Contains (peer)
                || AvailablePeers.Contains (peer)
                || BannedPeers.Contains (peer)
                || BusyPeers.Contains (peer);
        }

        internal void UpdatePeerCounts ()
        {
            int seeds = 0;
            int leeches = 0;
            for (int i = 0; i < ActivePeers.Count; i++) {
                if (ActivePeers[i].IsSeeder)
                    seeds++;
                else
                    leeches++;
            }

            Available = AvailablePeers.Count;
            Seeds = seeds;
            Leechs = leeches;
        }
    }
}
