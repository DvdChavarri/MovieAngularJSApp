using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using MovieAngularJSApp.Models;

namespace MovieAngularJSApp.Migrations
{
    [ContextType(typeof(MoviesAppContext))]
    partial class initial
    {
        public override string Id
        {
            get { return "20150913142833_initial"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta5-13549"; }
        }
        
        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("SqlServer:DefaultSequenceName", "DefaultSequence")
                .Annotation("SqlServer:Sequence:.DefaultSequence", "'DefaultSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .Annotation("SqlServer:ValueGeneration", "Sequence");
            
            builder.Entity("MovieAngularJSApp.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<string>("Director");
                    
                    b.Property<DateTime>("ReleaseDate");
                    
                    b.Property<decimal>("TicketPrice");
                    
                    b.Property<string>("Title");
                    
                    b.Key("Id");
                });
        }
    }
}
