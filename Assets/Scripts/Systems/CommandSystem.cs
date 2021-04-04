using System.Collections.Generic;
using Mirror;

namespace XPACE {

    public class CommandSystem : NetworkBehaviour {

        public static CommandSystem instance;

        private void Awake() {
            if (instance == null)
                instance = this;
        }
    }
}