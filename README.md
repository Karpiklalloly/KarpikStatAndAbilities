Система статов и абилок (пока только статов). Нацелен на ручное использование, в частности в ECS. По той же причине эффекты и бафы это структуры, дабы не нагружать кучу и легко интегрироваться в ецсы, которые используют структуры, а не классы.
На данный момент RangeStat надо обрабатывать вручную.

Используется кодген для статов, чтобы убрать прослойку в виде автосвойств и будущих возможных изменений.

Пример использования в "сыром" виде можно посмотреть в StatAndAbilities.Sample.

Тест-кейсы описаны в StatAndAbilities.Test.