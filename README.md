# Ticket
Mongo, EF, RabbitMQ,ElasticSearch,Kibana,SQL,Redis, .Net Core 5.0 , Generic Repository , HashService,EncryptionService,Css,Html,Javascript,Bootstrap,Ajax,Layered Architecture

Bu projede amaç kullanıcı,müşteri ,kullanıcı yetkilendirme ve açılan çağrıların düzenlenmesi, silinmesi belli başlı durumlara 
güncellenebilmesidir. Çoklu trafik senaryosu baz alınarak bir çağrı kapatıldığında kapalı çağrı bilgili kuyruğa gönderilir.
Burada kuyruğu dinleyen consumer tarafında mesaj okunur ve elasticsearch'e yazılır. Kapanan çağrılar üzerinde raporlama , analiz, arama
yapılacağını düşünürsek performans açısından elasticsearch terich edilmiştir. Elde bulunan hazır datalar ekranlara taşınırken performans için redis, müşteri bilgileri mongo üzerinde tutulmuştur.
