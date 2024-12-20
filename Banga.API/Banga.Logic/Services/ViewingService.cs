﻿using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;

namespace Banga.Logic.Services
{
    public class ViewingService : IViewingService
    {
        private readonly IViewingRepository _viewingRepository;
        public ViewingService(IViewingRepository viewingRepository)
        {
            _viewingRepository = viewingRepository;
        }

        public Task<long> CreateViewing(Viewing viewing)
        {
            return _viewingRepository.CreateViewing(viewing);
        }

        public Task DeleteViewing(long viewingId)
        {
            return _viewingRepository.DeleteViewing(viewingId);
        }

        public Task<IEnumerable<Viewing>> GetPropertyViewingsByUserId(int userId, long propertyId)
        {
            return _viewingRepository.GetPropertyViewingsByUserId( userId, propertyId);
        }

        public Task UpdateViewing(Viewing viewing)
        {
           return _viewingRepository.UpdateViewing(viewing);
        }
    }
}
