namespace TodoApp.Profiles
{
    public class TodoProfile : Profile {
        public TodoProfile() {
            // Source -> Target
            CreateMap<TodoItem, TodoReadDto>();
            CreateMap<TodoCreateUpdateDto, TodoItem>();
        }
    }