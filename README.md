# Ticket
Mongo, EF, RabbitMQ,ElasticSearch,Kibana,SQL,Redis, .Net Core 5.0 , Generic Repository , HashService,EncryptionService,Css,Html,Javascript,Bootstrap,Ajax,Layered Architecture

Bu projede amaç kullanıcı,müşteri ,kullanıcı yetkilendirme ve açılan çağrıların düzenlenmesi, silinmesi belli başlı durumlara 
güncellenebilmesidir. Çoklu trafik senaryosu baz alınarak bir çağrı kapatıldığında kapalı çağrı ilgili kuyruğa gönderilir.
Burada kuyruğu dinleyen consumer tarafında mesaj okunur ve elasticsearch'e yazılır. Kapanan çağrılar üzerinde raporlama , analiz, arama yapılacağını düşünülerek performans açısından elasticsearch terich edilmiştir.Caching mekanizması için redis, müşteri bilgilerinin tutulması için mongo tercih edilmiştir. Message Broker olarak RabbitMQ, Relational Database için SQL SERVER, dil olarak C# ve uygulama çatısı olarak .Net Core 5.0 tercih edilmiştir.
