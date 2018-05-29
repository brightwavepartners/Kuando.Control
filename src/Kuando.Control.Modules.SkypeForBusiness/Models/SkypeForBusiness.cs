using System.ComponentModel.Composition;
using System.Linq;
using Kuando.Control.Infrastructure.Events;
using Kuando.Control.Infrastructure.Models;
using Microsoft.Lync.Model;
using Prism.Events;

namespace Kuando.Control.Modules.SkypeForBusiness.Models
{
    [Export(typeof(ISkypeForBusiness))]
    public class SkypeForBusiness : ISkypeForBusiness
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly LyncClient _skypeClient;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public SkypeForBusiness(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._skypeClient = LyncClient.GetClient();

            this._skypeClient.ConversationManager.ConversationAdded += this.ConversationAdded;
            this._skypeClient.ConversationManager.ConversationRemoved += this.ConversationRemoved;

            this.SetColor();
        }

        private void ConversationAdded(object sender, Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs e)
        {
            this.SetColor();
        }

        private void ConversationRemoved(object sender, Microsoft.Lync.Model.Conversation.ConversationManagerEventArgs e)
        {
            this.SetColor();
        }

        private void SetColor()
        {
            this._eventAggregator.GetEvent<BusyLightColorEvent>()
                .Publish(this._skypeClient.ConversationManager.Conversations.Any() ? Color.Red : Color.Green);
        }

        #endregion
    }
}
