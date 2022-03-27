using System;
using System.Collections.Generic;

namespace Transsion.UtilitiesCrowd
{
    [Serializable]
    public class TranssionPayData
    {
        public TranssionPayList[] transsionPayLists;
    }

    [Serializable]
    public class TranssionPayList
    {
        public int aries_pay_status;
        public int online_pay_status;
        public string game_app_key;
        public List<TranssionPayProduct> product;
    }

    [Serializable]
    public class TranssionPayProduct
    {
        public string id;

        public string cp_id;

        public string name;

        public string gear_type;

        public string amount;
    }
}