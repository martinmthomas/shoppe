import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';

describe('UserService', () => {
  let service: UserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserService);
  });

  describe('getUserId', () => {
    it('should create a new userid if not exists', () => {
      localStorage.clear();

      const userId = service.getUserId();

      expect(userId.length).toBeGreaterThan(0);
    });

    it('should return same userId everytime', () => {
      const userId = service.getUserId();

      expect(service.getUserId()).toBe(userId);
      expect(service.getUserId()).toBe(userId);
      expect(service.getUserId()).toBe(userId);
    })
  });
});
