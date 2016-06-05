table.insert(function(context){
   var from = context.item.from;   
   context.tables("Users").read().then(function(ctx){
       var i = 0;
	   //TODO: find a better way to query table
       for(i=0;i<ctx.length;i++){
           if(ctx[i].id == from ){
               if(ctx[i].notificationId != undefined){
                    console.log( ctx[i].notificationId );
                    //TODO: Push to channel
               }      
               break;
           }
       }
         return ctx.execute();
    });

    return context.execute();
 });
 