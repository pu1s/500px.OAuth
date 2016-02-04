﻿namespace OAuth
{
    public partial class OAuthBroker
    {


        /// <summary>
        ///     Перечисление методов HTTP запроса
        /// </summary>
        public enum HttpMethods
        {
            /// <summary>
            ///     POST
            /// </summary>
            Post,

            /// <summary>
            ///     GET
            /// </summary>
            Get,

            /// <summary>
            ///     PUT
            /// </summary>
            Put,

            /// <summary>
            ///     DELETE
            /// </summary>
            Delete
        };
    }
}
