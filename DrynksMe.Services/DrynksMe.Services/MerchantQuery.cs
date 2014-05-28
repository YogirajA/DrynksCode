using System;
using System.Text;

namespace DrynksMe.Services
{
    public class MerchantQuery
    {
        
        private readonly StringBuilder _queryBuilder;
        public MerchantQuery()
        {
            _queryBuilder = new StringBuilder();
            _queryBuilder.Append(
                                  @"With seq as
                                   (
                                    SELECT tbl.*,ROW_NUMBER() OVER (ORDER BY ID ASC) rownum
	                                ,Count(*) OVER () Total
 	                                 FROM Merchant as tbl
	                                 where 1=1 ");
        }
        public MerchantQuery ByMerchantName()
        {
            _queryBuilder.Append(@"AND MerchantName like @merchantName ");
            return this;
        }
        public MerchantQuery ByProfileName()
        {
            _queryBuilder.Append(@"AND ProfileName like  @profileName ");
            return this;
        }

        public MerchantQuery ByTwitterHandle()
        {
            _queryBuilder.Append(@"AND TwitterHandle like  @twitterHandle ");
            return this;
        }

        public MerchantQuery ByVenueType()
        {
            _queryBuilder.Append(@"AND VenueType like  @venueType ");
            return this;
        }

        public string Bind()
        {
            _queryBuilder.Append(@")");
            _queryBuilder.Append(Environment.NewLine);
            _queryBuilder.Append(@"Select * from seq 
                                 WHERE seq.rownum BETWEEN @startRow AND @endRow
                                 ORDER BY seq.rownum");
            return _queryBuilder.ToString();
        }

       
    }
}
