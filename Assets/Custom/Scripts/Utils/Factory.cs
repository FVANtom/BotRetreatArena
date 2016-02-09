namespace com.terranovita.botretreat
{
    public static class Factory
    {
        public static T CreateDomainObjectFromJson<T>(JSONObject json) where T : ICreatableFromJson<T>, new()
        {
            var domainObject = new T();
            return domainObject.FromJson(json);
        }
    }
}